abstract class UIElement {
    constructor(){
    }
    
    protected htmlElement:HTMLElement;

    protected CreateDom(){
        this.htmlElement = this.CreateDomImpl();
    }
    
    protected abstract CreateDomImpl():HTMLElement;
    protected abstract RenderSelf();
    
    public Render(renderTo:HTMLElement){
        this.CreateDom();
        if(!this.htmlElement){
            return undefined;
        }
        
        this.RenderSelf();
        
        if (renderTo){
            renderTo.appendChild(this.htmlElement);
        }
    }
	
	public Hide(){
		if(!this.htmlElement){
			return;
		}
		this.htmlElement.style.display = 'none';
	}    
	
	public Show(show_style?: string){
		if(!this.htmlElement){
			return;
		}
		this.htmlElement.style.display = (!show_style)?'block':show_style;
	}    
}

export {UIElement}