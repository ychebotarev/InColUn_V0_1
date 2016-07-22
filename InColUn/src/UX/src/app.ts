/// <reference path='../typings/index.d.ts' />

import {Board} from './components/Board';
import {Dom} from './core/Dom'
import {CommandInfo,OnCommandCallback} from './core/CommandInfo'
import {SideBar} from './UIBlocks/SideBar'
import {ContentArea} from './UIBlocks/ContentArea'

import {ISection} from './components/interfaces'
import {TreeNodeData} from './lib/TreeMenu/TreeNodeData'

class App{
    private root:HTMLElement;
    
    private commands : { [key:string]:OnCommandCallback; } = {};
    
    private boards:Board[];
    private activeBoardId:string;
    
    private sidebar:SideBar;
    private contentArea:ContentArea;
    
    constructor(){
        this.sidebar = new SideBar();
        this.contentArea = new ContentArea();
        
        var nodes = [
                {
                    info:{title:'Misc Information', guid:'57be4c62-5ca1-4800-974f-11b7e92eda37'},
                    subNodes:[
                        {
							info:{title:'Statistic lactures', guid:'1d010db1-0980-4708-8c57-24d640717998'},
                            subNodes:[
                                {info:{title:'Lecture 1', guid:'74253706-fdef-4acc-9bd5-15c3ae7e5bf7'}},
                                {info:{title:'Definition', guid:'ece1cfeb-cac2-477d-94d9-5e880d21ea2d'}},
                                {info:{title:'Lecture 2', guid:'b0e7cd43-e9d6-4f20-9c40-da4b30947db3'}},
                                {info:{title:'Definition', guid:'c30ed5f5-075a-4e63-a4c3-8df587eb5d1c'}},
                                {info:{title:'Type of variables', guid:'83301322-95e8-41ac-ac4c-eac7a121a028'}},
                                {info:{title:'Correlation and measure', guid:'a93f94b5-27a0-4abb-85bc-16d0a397e86d'}}
                            ]
						},	
                        {
							info:{title:'Film and books', guid:'ee69e02c-9663-4a96-b6f9-3d289246f07d'},
                            subNodes:[
                                {info:{title:'Books to read', guid:'ae5d3e4b-191e-4da8-9cae-cf73731747a9'}},
								{info:{title:'Favourite book', guid:'db6c23d8-1408-493b-b4bd-74ff4e865edb'}},
								{info:{title:'Movies to watch', guid:'15ceda6a-2b36-45b4-a13e-87edf0ebd2cb'}}
							]
						},	
                        {
							info:{title:'Ukraine', guid:'0cdc9c6c-d7d9-41c2-9c7d-4666fb702b37'},
                            subNodes:[
                                {info:{title:'Odessa, what happen on may 2', guid:'5c47797a-e4b2-4fbe-ba9f-23a4989c10ae'}},
								{info:{title:'Ukrainian nationalists', guid:'c932280f-55d3-454f-b47e-9625d0e5f16d'}},
								{info:{title:'Civil war at Ukraine', guid:'2de1a32b-db8d-42a8-a38d-4a0ce00560e5'}},
								{info:{title:'Референдум', guid:'9e77cf29-538b-4a8b-8f2a-9b21c572b9d8'}},
								{info:{title:'Знаковые видео', guid:'d850eff7-16f2-4ae3-bba3-da88b7d54f32'}},
								{info:{title:'Фейки украинских СМИ', guid:'295235b3-19fe-4569-93e2-0983bc4c99e8'}},
								{info:{title:'Разное', guid:'f19f4923-01a4-4687-9b86-3d71215717f5'}},
								{info:{title:'Порошенко', guid:'839274a8-2aa6-4b55-a204-c23357119076'}},
								{info:{title:'ВСУ', guid:'e722f740-60d0-4046-b54c-fe2071305784'}}
							]
						},	
                        {
							info:{title:'Interview questions', guid:'2f3d4139-adad-42ec-8c99-f7a820eab9a9'},
                            subNodes:[
                                {info:{title:'Google interview questions', guid:'d5220776-cd55-4eb4-8b31-97eaecbdda74'}},
								{info:{title:'More standard questions info', guid:'64efb990-09da-43a3-ae29-6aca1db6e20e'}},
								{info:{title:'Истории для интервью', guid:'a4c7f3ba-728e-435a-bde9-10d965947b5b'}},
								{info:{title:'Топики для интервью', guid:'dfb066ed-5b04-4b06-a500-4de42fd4cc27'}},
								{info:{title:'Multithreading', guid:'024c0c57-2689-4191-a31e-445b321c9675'}},
								{info:{title:'Not job related questions', guid:'86660a4e-278e-40a1-b274-e93b661583ce'}},
							]
						},	
                        {info:{title:'Food questions', guid:'2e3b69d6-aa36-4d6a-98d2-d2e48769a446'}},	
                        {
							info:{title:'Job search', guid:'aadfc7ee-96ba-40ca-a2c8-5c513c81776b'},
                            subNodes:[
                                {info:{title:'Current process', guid:'c8d2e0da-97d0-4a7d-8481-23703591c15a'}},
								{info:{title:'Important algorithm', guid:'679ed614-9ed2-460d-9689-2b6d56b6f47f'}},
								{info:{title:'c++ interview questions', guid:'706e1339-bde1-4dca-8fc8-262d0d90fbe7'}}
							]
						},	
                        {info:{title:'Training - Jym', guid:'93652d5c-f467-4f42-ab1b-ee792b638a20'}},	
                        {info:{title:'Team Leading', guid:'ae8ce805-cacf-47f0-a4b5-9757b0df3c47'}},	
					]
                },
                {
                    info:{title:'GTD', guid:'2af35ed7-8479-4b89-87eb-82353d3601b5'},
                    subNodes:[
                        {
                            info:{title:'Tasks', guid:'4e665ff3-5603-4321-b46e-53f64901d03a'},
                            subNodes:[
                                {info:{title:'child_level3_a', guid:'162dc256-f289-4b92-a9e6-682ce6c88545'}},
                                {info:{title:'child_level3_b', guid:'ab38fe3d-8a48-4ac6-b71f-71c924116269'}}
                            ]
                        },
                        {info:{title:'Weekly review', guid:'f97e0251-3765-46c6-8b8a-0bd331c10db2'}},
                        {info:{title:'Backlog', guid:'e5c88949-025a-4d62-b694-f3f4d64802d8'}},
                        {info:{title:'Project List', guid:'5253a0a6-cf8b-4aa9-82dc-f9cff3bc0c1c'}}
                    ]
                },
                {
                    info:{title:'Recycled Boards', guid:'50849231-ec86-44ab-93bc-61f197e240d5',commandInfo:{command:'OpenRecycledBoards'}}
                }
        ];
		this.sidebar.LoadTreeContainer(nodes);
        let openPageCallback:OnCommandCallback = (param1:{}, param2:{}) =>{ this.contentArea.OnOpenPage(param1['guid'])}
		
		this.SetCommandDispatcher('OpenBoard', (param1:{}, param2:{}) =>{ this.OnOpenBoard(param1['guid'])});
        this.SetCommandDispatcher('OpenPage', openPageCallback);
		this.SetCommandDispatcher('OpenBoards', () => { this.contentArea.OnOpenBoards()});
		this.SetCommandDispatcher('OpenSavedBoards', () => { this.contentArea.OnOpenSavedBoards()});
		this.SetCommandDispatcher('OpenRecycledBoards', () => { this.contentArea.OnOpenRecycledBoards()});
    }
    
	public SetCommandDispatcher(key:string, command:OnCommandCallback){
        this.commands[key]=command;
    }
    
    public RemoveCommandDispatcher(key:string){
        this.commands[key] = undefined;
    }
    
    public OnCommand(commandInfo:CommandInfo){
        var dispatch = this.commands[commandInfo.command];
        if (dispatch === undefined){
            console.log("Can't find handle for command: "+commandInfo.command);
        }
		else {        
        	dispatch(commandInfo.param1, commandInfo.param2);
		}
    }
    
    public SetRoot(root:HTMLElement){
        this.root = root;
    }

    public LoadBoardInfo(user:string){
        
    }
    
    public  AddBoard(board:Board){
        this.boards[board.getID()] = board;    
    }
    
    public Render(){
        var sidebar = document.getElementById('sidebar_menu');
        console.log('rendering');
        if (sidebar != undefined){
            this.sidebar.RenderTo(sidebar);
        }
        else{
            console.log("can't find sidebar");
        }
        
        var content_area = document.getElementById('content_area');
        if (content_area != undefined){
            this.contentArea.Render(content_area);
        }
        else{
            console.log("can't find content_area");
        }
    }
	
	public Initialize(){
		this.sidebar.OnLoadRecentBoards();
		this.contentArea.OnOpenBoards();
	}
	
	public OnOpenBoard(id:string){
		$.ajax('/api/board/'+id, {
			type     : 'GET', 
			data     : {}, 
			dataType : 'json',
			success  : (data: any, textStatus: string, jqXHR: JQueryXHR) => {this.OnBoardLoaded(data, textStatus, jqXHR)}
		});  
	}
	
	private OnBoardLoaded(data: any, textStatus: string, jqXHR: JQueryXHR) {
		if (!data || data.success != true){
			return;
		}
		var sections:ISection[] = data.sections;
		var nodes = this.SectionsToNodes(sections);
		//this.sidebar.LoadTreeContainer(nodes);
	}	
    
    private  SectionsToNodes(sections:ISection[]):TreeNodeData[]{
        return null;   
    }
}

let application = new App();

export {application}