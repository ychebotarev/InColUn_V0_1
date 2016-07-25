import {Dom} from '../core/dom';
import {UIElement} from '../core/UIElement';
import {Box} from './Box';

class Section extends UIElement{
    public id:string;
    public title:string;
    private boxes:Box[];
    
    constructor(){
        super();
    }
    
    protected CreateDomImpl():HTMLElement{
        return Dom.div();
    }
    
    protected RenderSelf(){
        this.RenderHeader(this.htmlElement);
        this.boxes.forEach(box => box.Render(this.htmlElement));
    }    
    
    private RenderHeader(root:HTMLElement){
        
    }
}

export {Section}