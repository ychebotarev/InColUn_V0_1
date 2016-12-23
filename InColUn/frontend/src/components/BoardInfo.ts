import {Dom} from '../core/dom'
import {DomElement} from '../core/DomElement';
import {IBoard} from './interfaces'
import {application} from '../App';

export class BoardInfo extends DomElement{
    private board:IBoard;
    
    constructor(board:IBoard){
        super({tag:'div', className:'board-item'});
        this.board = board;
    }
    
    Id():string{
        return this.board.id;
    }
    
	Title():string{
        return this.board.title;
    }
    
	protected RenderSelf(self:HTMLElement){
		var img = Dom.img('images/default_board.png');
		self.appendChild(img)
		
		var title = Dom.div();
		title.innerText = this.Title(); 
		
		self.appendChild(title);
        self.onclick = (ev:MouseEvent) => { application.OnCommand({command:'OpenBoard', param1:{guid:this.Id()}})};
    }
}