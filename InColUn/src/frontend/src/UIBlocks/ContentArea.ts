import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement'
import {BoxDimentions, Box} from '../components/Box'
import {BoardsContext} from './BoardsContext'
import {BoxContext} from './BoxContext'
import {application} from '../App';

export class ContentArea {
	private htmlElement:HTMLElement;

	private boxContext:BoxContext;
	private boardsContext:BoardsContext;
	
    constructor(){
    	this.boxContext = new BoxContext();
		this.boardsContext = new BoardsContext();

		//treeheader.onclick = (ev:MouseEvent) => { application.OnCommand({command:'OpenPage', param1:{guid:this.info.guid}});
	}
	
	public OnOpenBoards(){
		this.boxContext.Hide();
		
		this.boardsContext.LoadBoards();
	}

	public OnOpenSavedBoards(){
	}
    
	public OnOpenRecycledBoards(){
	}
	
	public OnOpenPage(guid:string){
		//this.boardsContext.Hide();
		//this.boxContext.LoadPage(guid);
		//this.boxContext.Show();
	}

    private OnBoxActivated(guid:string){
    }
    
    private OnBoxDeactivated(guid:string){
    }
    
    private OnBoxSizeChanged(guid:string, dimentions:BoxDimentions){
    }
    
    private OnBoxContentChanged(guid:string){
        
    }

	public SetRoot(_root:HTMLElement){
		this.htmlElement = _root;
		//self.onclick = (ev:MouseEvent) => { application.OnCommand({command:'HideSideBar'})};
	}

    public Render(){
		this.boxContext.Render(this.htmlElement);
		this.boardsContext.Render(this.htmlElement);
		this.boxContext.Hide();
		//this.boardsContext.Show('flex');
    }
} 