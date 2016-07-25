import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement'
import {TreeContainer} from '../lib/TreeMenu/TreeContainer'
import {TreeNodeData} from '../lib/TreeMenu/TreeNodeData'

export class SideBar {
	private htmlElement:HTMLElement;
    private treeContainer:TreeContainer;
    
    constructor(){
    }
    
    public LoadTreeContainer(nodes:TreeNodeData[]){
        this.treeContainer = new TreeContainer(nodes);
    }
    
    public AddTreeNode(node:TreeNodeData){
        this.treeContainer.AddNode(node);
    }

	public SetRoot(sidebar:HTMLElement){
		this.htmlElement = sidebar;
	}
    
    public Render(){
        this.treeContainer.Render(this.htmlElement);
    }
	
	public OnLoadRecentBoards(){
		this.treeContainer.setLoading(true);
		$.ajax('/api/v1.0/recent', {
			type     : 'GET', 
			data     : {}, 
			dataType : 'json',
			success  : (data: any, textStatus: string, jqXHR: JQueryXHR) => {this.OnRecentLoaded(data, textStatus, jqXHR)}
		});  
	}

	public ShowSideBar(){
		this.htmlElement.style.width = '350px';
	}

	public HideSideBar(){
		this.htmlElement.style.width = '0px';
	}
	
	private OnRecentLoaded(data: any, textStatus: string, jqXHR: JQueryXHR) {
		this.treeContainer.setLoading(false);
		
		if(data.recent && data.recent.length > 0){
			//data.boards.forEach(doc => { this.boardsInfo[doc.guid] = new BoardInfo(doc) });
		}
	}
}