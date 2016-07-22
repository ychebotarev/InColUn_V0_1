import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement'
import {BoxDimentions, Box} from '../components/Box'
import {BoardsContext} from './BoardsContext'
import {BoxContext} from './BoxContext'

export class ContentArea {
	private boxContext:BoxContext;
	private boardsContext:BoardsContext;
	
    constructor(){
    	this.boxContext = new BoxContext();
		this.boardsContext = new BoardsContext();
	}
	
	public OnOpenBoards(){
		this.boxContext.Hide();
		//this.boardsContext.Show('flex');
		
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

    public Render(contentArea:HTMLElement){
		this.boxContext.Render(contentArea);
		this.boardsContext.Render(contentArea);
		this.boxContext.Hide();
		//this.boardsContext.Show('flex');
    }
} 