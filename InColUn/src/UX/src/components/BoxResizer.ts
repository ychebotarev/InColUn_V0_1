import {DomElement} from '../core/DomElement';

export enum ResizeDirection{
    Horz,
    Vert,
    Both
}

export interface RisizerProps{
    direction:ResizeDirection;
    onResizeStart?:(direction:ResizeDirection, clientX:number, clientY:number)=>void;
}

export class BoxResizer extends DomElement{
    private resizerProps:RisizerProps;
    
    constructor(resizerProps:RisizerProps) {
        super({
            tag:'div', 
            className: BoxResizer.getClass(resizerProps.direction)
        });
        this.resizerProps = resizerProps;
    }
    
    onTouchStart  = (event:TouchEvent) =>{
        var te=event.touches[0];  
        if (this.resizerProps.onResizeStart){
            this.resizerProps.onResizeStart(this.resizerProps.direction, te.clientX, te.clientY);
        }
    }
    
    onResizeStart = (event:MouseEvent) =>{
        if (this.resizerProps.onResizeStart){
            this.resizerProps.onResizeStart(this.resizerProps.direction, event.clientX, event.clientY);
        }
    }
    
    public GetDirection():ResizeDirection{
        return this.resizerProps.direction
    }
    
    public static getClass(type:ResizeDirection):string {
        switch(type){
            case ResizeDirection.Horz:
                return 'resizer_horz';
            case ResizeDirection.Vert:
                return 'resizer_vert';
            case ResizeDirection.Both:
                return 'resizer_both';
        }
    }
  
    RenderSelf(self:HTMLElement) {
        self.onmousedown = this.onResizeStart;
        self.ontouchstart = this.onTouchStart;
    }
}