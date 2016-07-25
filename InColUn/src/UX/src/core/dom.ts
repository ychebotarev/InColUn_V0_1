import {DomElementProps} from './DomElementProps';

class Dom{
    public static CreateFromProps(props:DomElementProps):HTMLElement{
        return Dom.Create(props.tag, props.className, props.style, props.attributes);
    }
    
    public static Create(tag:string, className?:string, style?: {}, attributes?:{}):HTMLElement{
        var element = document.createElement(tag);
        if (className){
            element.className = className;
        }
        if (attributes){
            for(var attrName in attributes){
                if(attrName == 'style'){
                    var style_attr = attributes['style'];
                    for(var styleName in style_attr){
                        element.style[styleName]=style_attr[styleName];
                    }
                }
                else{
                    element.setAttribute(attrName, attributes[attrName]);
                }
            }
        }
        
        if(style){
            for(var styleName in style){
                element.style[styleName]=style[styleName];
            }            
        }
        return element;
    }
    
    public static element(tag:string, className?:string, style?: {}, attributes?:{}):HTMLElement{
        return Dom.Create(tag, className, style, attributes);
    }

    public static a(className?:string, style?: {}, attributes?:{}):HTMLAnchorElement{
        return <HTMLAnchorElement>Dom.Create('a', className, style, attributes);
    }
    
    public static div(className?:string, style?: {}, attributes?:{}):HTMLElement{
        return Dom.Create('div', className, style, attributes);
    }
    public static span(className?:string, style?: {}, attributes?:{}):HTMLElement{
        return Dom.Create('span', className, style, attributes);
    }
    
    public static ul(className?:string, style?: {}, attributes?:{}):HTMLElement{
        return Dom.Create('ul', className, style, attributes);
    }
    public static li(className?:string, style?: {}, attributes?:{}):HTMLElement{
        return Dom.Create('li', className, style, attributes);
    }
	
	public static img(path:string, className?:string){
		return Dom.Create('img',className, {}, {src:path} )
	}
    
    public static text(content:string, tag:string,className?:string, style?: {}, attributes?:{}):HTMLElement{
        var el = Dom.Create(tag, className, style, attributes);
        el.innerText = content;
        return el;
    }
    
    public static bootstrap_icon(icon:string){
        var cn:string = 'glyphicon glyphicon-'+icon;
        return Dom.Create('i', cn);
    }
	
	public static GetElementByClassName(parent:HTMLElement, className:string):HTMLElement{
		if(!parent){
			return undefined;
		}
		var elements = parent.getElementsByClassName(className);
		if(!elements || elements.length == 0){
			return undefined;
		}
		return <HTMLElement>elements[0];
	}
	
	public static Hide(element:HTMLElement){
		element.style.display = 'none';
	}
	public static Show(element:HTMLElement){
		element.style.display = 'block';
	}
}


export {Dom};