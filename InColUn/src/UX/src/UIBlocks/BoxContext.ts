import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement'
import {BoxDimentions, Box} from '../components/Box'

export class BoxContext extends UIElement {
	isLoading:boolean;
    private activeBox:Box;
    private boxes:{[key:string]:Box};
    
    constructor(){
		super();
		this.isLoading = false;
        this.boxes = {};
		this.createFakeBoxes();//TODO - delete        
    }

	public LoadPage(guid:string){
		this.isLoading = true;
		$.ajax('/api/section:'+guid, {
			type     : 'GET', 
			data     : {}, 
			dataType : 'json',
			success  : (data: any, textStatus: string, jqXHR: JQueryXHR) => {this.OnPageLoaded(data, textStatus, jqXHR)}
		});  
	}
	
	private OnPageLoaded(data: any, textStatus: string, jqXHR: JQueryXHR){
		this.isLoading = false;
		this.createFakeBoxes();	
	}
	
    protected CreateDomImpl():HTMLElement{
		var container = Dom.div('content-container');
		
		//create title
		
		//create toolbar
		
		return container;
	}

    protected RenderSelf(){
        for(var guid in this.boxes){
            this.boxes[guid].Render(this.self);
        }
	}
	
    private OnBoxActivated(guid:string){
    }
    
    private OnBoxDeactivated(guid:string){
    }
    
    private OnBoxSizeChanged(guid:string, dimentions:BoxDimentions){
    }
    
    private OnBoxContentChanged(guid:string){
        
    }
	
	private createFakeBoxes(){
        
		let onBoxActivated = (guid:string) => { this.OnBoxActivated(guid);}
        let onBoxDeactivated = (guid:string) => { this.OnBoxDeactivated(guid);}
        let onBoxSizeChanged = (guid:string, dimentions:BoxDimentions) => { this.OnBoxSizeChanged(guid, dimentions);}
        let onBoxContentChanged	 = (guid:string) => { this.OnBoxContentChanged(guid)}

        for(var i:number = 0; i< 5; ++i){
            this.boxes['box1'+String(i)] = new Box({
                dimention:{x:100+10*i,y:100+15*i,w:200,h:40}, 
                info:{guid:'box1'+String(i)},
                callbacks:{
                    boxActivated: onBoxActivated,
                    boxDeactivated: onBoxDeactivated,
                    sizeChanged: onBoxSizeChanged,
                    contentChanged: onBoxContentChanged}});
        }

        this.boxes['box22'] = new Box({
            dimention:{x:100,y:300,w:220,h:40}, 
            info:{guid:'box2'},
            callbacks:{
                boxActivated: onBoxActivated,
                boxDeactivated: onBoxDeactivated,
                sizeChanged:onBoxSizeChanged,
                contentChanged:onBoxContentChanged}});
		
	}
} 