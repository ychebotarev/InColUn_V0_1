import {Dom} from './dom'
import {DomElementProps} from './DomElementProps';
import {DomElement} from './DomElement'

class DomContainer extends DomElement{
    childrens:DomElement[];
    constructor(props:DomElementProps){
        super(props);
        this.childrens = [];
    }
    
    public AddChild(child:DomElement){
        this.childrens.push(child);
    }
    
    protected RenderSelf(self:HTMLElement){
        this.childrens.forEach(child => { child.Render(self);})    
    }    
}

function RenderDomElement(props:DomElementProps,renderTo:HTMLElement):HTMLElement{
    var element = new DomElement(props);
    return element.Render(renderTo);
}

export {DomContainer}