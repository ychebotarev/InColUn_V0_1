import {UIElement} from './UIElement';
import {Dom} from './dom';

abstract class UIContainer extends UIElement{
    children:UIElement[];
    
    constructor(){
        super();
        this.children = [];
    }
    public AddChild(child:UIElement){
        this.children.push(child);
    }

    protected CreateDomImpl():HTMLElement{
        return Dom.div();
    }
    
    protected RenderSelf(){
        this.children.forEach(c=>c.Render(this.self));            
    };
}

export {UIContainer}