/// <reference path='../../typings/index.d.ts' />
import {Dom} from '../core/dom'
import {UIElement} from '../core/UIElement'
import {BoardInfo} from '../components/BoardInfo'
import {application} from '../App';

export class BoardsContext extends UIElement {
	isLoading: boolean;
    boardsInfo: { [key: string]: BoardInfo };

    constructor() {
		super();
		this.isLoading = false;
		this.boardsInfo = {};
	}

	public LoadBoards() {
		this.isLoading = true;
		$.ajax('/api/v1.0/boards', {
			type: 'GET',
			data: {},
			dataType: 'json',
			success: (data: any, textStatus: string, jqXHR: JQueryXHR) => { this.OnBoardsLoaded(data, textStatus, jqXHR) }
		});
	}

	protected CreateDomImpl(): HTMLElement {
		var container = Dom.div('board-container');

		//create title

		//create toolbar

		return container;
	}

    protected RenderSelf() {
		if (this.isLoading) {
			this.RenderLoadingState(this.htmlElement);
		}
		else {
			this.RenderBoards(this.htmlElement);
		}
    }

	private OnBoardsLoaded(data: any, textStatus: string, jqXHR: JQueryXHR) {
		this.isLoading = false;
		this.boardsInfo={}
		if(data.boards && data.boards.length > 0){
			data.boards.forEach(board => { this.boardsInfo[board.id] = new BoardInfo(board) });
		}
		this.htmlElement.innerHTML='';
		this.RenderSelf();
	}

	private RenderBoards(self: HTMLElement) {
		var spinner = Dom.GetElementByClassName(this.htmlElement, 'boards-spinner');
		if (spinner) {
			Dom.Hide(spinner);
		}

		var add_board = Dom.div('board-item add-board');
		add_board.innerHTML = '  <span>Create New Board</span><div><img src="images/new_board.png"></div><i class="btn" data-toggle="modal" data-target="#newBoardModal">+</i>';
		self.appendChild(add_board);
        add_board.onclick = (ev:MouseEvent) => { application.OnCommand({command:'NewBoardDialog'})};
		
		for (var key in this.boardsInfo) {
			this.boardsInfo[key].Render(self);
		}
	}

	private RenderLoadingState(self: HTMLElement) {
		var spinner = Dom.GetElementByClassName(self, 'boards-spinner');
		if (spinner) {
			Dom.Show(spinner);
			return;
		}
		var holder = Dom.div('boards-spinner centered-box');
		var outer_ball = Dom.div('spinning_ball outer_ball');
		var inner_ball = Dom.div('spinning_ball inner_ball');
		holder.appendChild(outer_ball);
		holder.appendChild(inner_ball);
		self.appendChild(holder);
	}
}