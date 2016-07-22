import {DomElement} from './DomElement';
import {DomElementProps} from './DomElementProps';

class DomElementText extends DomElement{
    
    constructor(public text:string, props:DomElementProps){
        super(props);
    }
    
    protected RenderSelf(self:HTMLElement){
        self.innerText = this.text;
    }        
}

export {DomElementText}