import {DomElement} from '../core/DomElement';

export interface BoxDraggerProps{
    onDragStart?:(clientX:number, clientY:number)=>void;
}

export class BoxDragger extends DomElement{
    private draggerProps:BoxDraggerProps;
    
    constructor(draggerProps:BoxDraggerProps) {
        super({
            tag:'div', 
            className: BoxDragger.getClass()
        });
        
        this.draggerProps = draggerProps;
    }
    
    public static getClass():string{
        return 'box_dragger';
    }
    
    onTouchStart  = (event:TouchEvent) =>{
        var te=event.touches[0];  
        if (this.draggerProps.onDragStart){
            this.draggerProps.onDragStart(te.clientX, te.clientY);
        }
    }
    
    onDragStart = (event:MouseEvent) =>{
        console.log('onDragStart');
        if (this.draggerProps.onDragStart){
            console.log('firing dragStart');
            this.draggerProps.onDragStart(event.clientX, event.clientY);
        }
    }
    
    RenderSelf(self:HTMLElement) {
        self.onmousedown = this.onDragStart;
        self.ontouchstart = this.onTouchStart;
    }
}