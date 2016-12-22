/// <reference path="../internal_typings/medium-editor.d.ts" />

import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement';
import {ResizeDirection, RisizerProps, BoxResizer} from './BoxResizer';
import {BoxDraggerProps, BoxDragger} from './BoxDragger';

export interface BoxDimentions{
    x:number;
    y:number;
    w:number; 
    h:number;
}

export interface BoxInfo{
    guid:string;
}

export interface BoxCallbacks{
    boxActivated?:(guid:string) => void;
    boxDeactivated?:(guid:string) => void;
    sizeChanged?:(guid:string, dimentions:BoxDimentions) => void;
    contentChanged?:(guid:string) => void;
}

export interface BoxProps{
    dimention:BoxDimentions;
    info:BoxInfo;
    callbacks:BoxCallbacks;
} 

enum BoxAction{
    none,
    resize,
    drag
} 

class BoxState{
    dimentions:BoxDimentions;
    original:BoxDimentions;
    action:BoxAction;
    direction:ResizeDirection;
    activated:boolean;
}

export class Box extends UIElement{
    private info:BoxInfo;
    private state:BoxState;
    private callback:BoxCallbacks;
    
    private dragger:BoxDragger;
    private horzResize:BoxResizer;
    
    public constructor(props:BoxProps){
        super();
        this.info=props.info;
        this.state = new BoxState();
        this.state.dimentions = props.dimention;
        this.state.action = BoxAction.none;
        this.state.activated = false;
        this.callback = props.callbacks;
        
        let onResizeStart = (direction:ResizeDirection, clientX:number, clientY: number) => {
                this.OnResizeStart(direction, clientX, clientY)
        }
        
        let onDragStart = (clientX:number, clientY: number) => {
            this.OnDragStart(clientX, clientY);
        }
            
        let onTouchMove = (ev:TouchEvent) => {this.OnTouchMove(ev);};
        
        let onMouseMove = (ev: MouseEvent) => {this.OnMouseMove(ev.clientX, ev.clientY);};
        let onMouseUp = (ev: MouseEvent) => {this.OnMouseUp(ev);};            
        
        this.horzResize = new BoxResizer({ direction:ResizeDirection.Horz, onResizeStart});
        this.dragger = new BoxDragger({onDragStart});
        
        window.addEventListener('mouseup', onMouseUp);
        window.addEventListener('mousemove', onMouseMove);
        window.addEventListener('touchmove', onTouchMove);
        window.addEventListener('touchend', onMouseUp);
    }
    
    public activate(){
        if(this.state.activated == true){
            return;
        }
        this.setActivatedState(true);
    }
    
    public deactivate(){
        this.setActivatedState(false);
    }
    
    protected setActivatedState(activated:boolean){
        this.state.activated = activated;
        this.changeActivationState();
        if (this.state.activated == true && this.callback.boxActivated){
            this.callback.boxActivated(this.info.guid);
        }
        if (this.state.activated == false && this.callback.boxDeactivated){
            this.callback.boxDeactivated(this.info.guid);
        }
    }
    
    protected changeActivationState(){
        var resizer = <HTMLElement>this.htmlElement.getElementsByClassName(BoxResizer.getClass(this.horzResize.GetDirection()))[0];
        if(resizer != undefined){
            resizer.style.display = this.state.activated?'block':'none'; 
        }
        
        var dragger = <HTMLElement>this.htmlElement.getElementsByClassName(BoxDragger.getClass())[0];
        if(dragger != undefined){
            dragger.style.display = this.state.activated?'block':'none'; 
        }
    }
    
    protected OnTouchMove(ev: TouchEvent){
        this.OnMouseMove(ev.touches[0].clientX, ev.touches[0].clientY);
    }
    
    protected OnResize(clientX:number, clientY:number){
        const { dimentions, original, action, direction } = this.state;
        
        let newWidth = original.w;
        
        if (direction == ResizeDirection.Horz || direction == ResizeDirection.Both) {
            newWidth = original.w + clientX - original.x;
        }
        
        if(newWidth == dimentions.w){
            return;
        }
        this.state.dimentions.w = newWidth;
        
        this.SetDimentions();
        
        window.requestAnimationFrame((timestamp:any) => { this.OnUpdateHeight()});
    }
    
    protected OnDrag(clientX:number, clientY:number){
        const { dimentions, original, action, direction } = this.state;

        let newX = dimentions.x + clientX - original.x;
        let newY = dimentions.y + clientY - original.y;
        
        if(newX == dimentions.x && newY == dimentions.y){
            return;
        }
        this.state.dimentions.x = newX;
        this.state.dimentions.y = newY;

        this.state.original.x = clientX;
        this.state.original.y = clientY;
        
        this.SetDimentions();
    }

    protected OnMouseMove(clientX:number, clientY:number){
        if (this.state.action == BoxAction.none){
            return;
        }
        
        if(this.state.action == BoxAction.resize){
            this.OnResize(clientX, clientY);
        }
        
        if(this.state.action == BoxAction.drag){
            this.OnDrag(clientX, clientY);
        }
    }
    
    protected OnMouseUp(ev:MouseEvent){
        if (this.state.action == BoxAction.none){
            return;
        }
        
        if(this.state.action == BoxAction.resize && this.callback.sizeChanged){
            this.callback.sizeChanged(this.info.guid, this.state.dimentions);
        }
        
        if(this.state.action == BoxAction.drag && this.callback.sizeChanged){
            this.callback.sizeChanged(this.info.guid, this.state.dimentions);
        }
        
        this.state.action = BoxAction.none;
    }
    
    protected OnMouseEnter(){
        this.activate();   
    }
    
    protected OnMouseLeave(){
        this.deactivate();    
    }
    
    protected SetDimentions(){
        this.htmlElement.style.left = String(this.state.dimentions.x)+'px';
        this.htmlElement.style.top = String(this.state.dimentions.y)+'px';
        
        this.htmlElement.style.width = String(this.state.dimentions.w)+'px';
        this.htmlElement.style.height = String(this.state.dimentions.h)+'px';
    }
    
    protected CreateDomImpl():HTMLElement{
        var box = Dom.div('internal-box');
                
        box.onmouseenter = (ev:MouseEvent) => { this.OnMouseEnter();}
        box.onmouseleave = (ev:MouseEvent) => { this.OnMouseLeave();}

        return box;
    }
    
    protected OnContentChanged(content:HTMLElement){
        var style = window.getComputedStyle(content, null);
        if (parseInt(style.height) + 20 != this.state.dimentions.h)
        {
            this.state.dimentions.h = parseInt(style.height) + 20;
            this.htmlElement.style.height = String(this.state.dimentions.h)+'px';
        }
    }
    
    protected OnUpdateHeight(){
        var content = this.htmlElement.getElementsByClassName('internal-box-content')[0];
        var style = window.getComputedStyle(content, null);
        if (parseInt(style.height) + 20 != this.state.dimentions.h)
        {
            this.state.dimentions.h = parseInt(style.height) + 20;
            this.state.original.h = this.state.dimentions.h;
            this.htmlElement.style.height = String(this.state.dimentions.h)+'px';
        }
    }
    
    protected RenderSelf(){
        this.SetDimentions();
        this.horzResize.Render(this.htmlElement);
        this.dragger.Render(this.htmlElement);
        
        var content=Dom.div('internal-box-content');
        this.htmlElement.appendChild(content);
        var editor = new MediumEditor([content],
        {
            buttonLabels: 'fontawesome',
            toolbar: {
                buttons: [
                    'bold',
                    'italic',
                    'table'
                ]
            }
            /*,  
            extensions: {
                table: new MediumEditorTable()
            } */                     
        });
        editor.subscribe('editableInput', (data:any, editable:HTMLElement) => { this.OnContentChanged(editable)})
    }
    
    protected OnDragStart(clientX:number, clientY: number){
        
        this.state.action = BoxAction.drag;
        this.state.original = {x : clientX, y : clientY, w : this.state.dimentions.w, h : this.state.dimentions.h}
    }
    
    protected OnResizeStart(direction:ResizeDirection, clientX:number, clientY: number) {
        
        this.state.action = BoxAction.resize;
        this.state.direction=direction;
        this.state.original = {x : clientX, y : clientY, w : this.state.dimentions.w, h : this.state.dimentions.h}
    }
}