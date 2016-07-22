import {Dom} from '../../core/dom';
import {classNames} from '../../lib/classNames';
import {UIElement} from '../../core/UIElement';
import {DomElement,RenderDomElement} from '../../core/DomElement';
import {TreeNodeInfo, TreeNodeData} from './TreeNodeData'
import {CommandInfo} from '../../core/CommandInfo'
import {application} from '../../App';

export class TreeNode extends UIElement{
    private info:TreeNodeInfo;
    private level:number;
    private open:boolean;
    private nodes:TreeNode[];
    
    constructor(nodeInfo:TreeNodeData, level:number){
        super();
        this.info = nodeInfo.info;
        this.level = level;
        this.nodes = [];
        this.open=true;
        if(nodeInfo.subNodes){
            nodeInfo.subNodes.forEach(
                node =>{
                    this.nodes.push(new TreeNode(node, level+1));
                } 
            )
        }
    }
    
    public ToggleSubTree(){
        var treenode = document.getElementById('tn_'+this.info.guid);
        if (!treenode){
            return;
        }
        var toggle_icon = <HTMLElement>(treenode.getElementsByClassName('tree-toggle-icon')[0]);
        var treelist = <HTMLElement>(treenode.getElementsByClassName('treelist')[0]);
        if(!toggle_icon || !treelist){
            console.log('error getting childs for id:'+this.info.guid);
            return;
        }
        
        this.open = !this.open;
        
        treelist.style.display = this.open? 'block':'none';
        toggle_icon.className = 'tree-toggle-icon fa fa fa-stack-1x fa-chevron-'+(this.open ? 'up':'down');
        treelist.style.display = this.open? 'block' : 'none';
    }
    
    protected CreateDomImpl():HTMLElement{
        var node = Dom.Create('div','treenode'); 
        node.id = 'tn_'+this.info.guid;
        return node;
    }
    
    protected RenderSelf(){
        var header = this.RenderHeader();
        var childs = this.RenderChilds();
        
        this.self.appendChild(header);
        if (childs != undefined){
            this.self.appendChild(childs);
        }
    }
    
    private getIconClass(){
        if(this.nodes && this.nodes.length > 0){
            return 'fa fa-book';
        }
        return 'fa fa-file-text-o';
    }
    
    private RenderHeader():HTMLElement{
        var treeheader = Dom.div('treeheader');
        
        treeheader.appendChild(
            Dom.element('i', "treeitem-icon "+ this.getIconClass(), {color:'rgb(241,202,93)'}));
        treeheader.appendChild(
            Dom.text(this.info.title,'span'));
        if(this.nodes && this.nodes.length > 0)
        {
            
            var span = Dom.span("tree-toggle fa-stack fa-lg");
            span.appendChild(Dom.element('i',"tree-toggle-bgnd fa fa-circle fa-stack-2x"));
            var icon = Dom.element('i', 'tree-toggle-icon fa fa-stack-1x fa-chevron-'+(this.open ? 'up':'down'), {color:'grey'})
            span.appendChild(icon);
            
            
            span.onclick = (ev:MouseEvent) => {
                this.ToggleSubTree();
            }
            treeheader.appendChild(span);
        }
        if(this.level > 0){
            treeheader.style.cursor='pointer';
            treeheader.onclick = (ev:MouseEvent) => { application.OnCommand({command:'OpenPage', param1:{guid:this.info.guid}});
            }
        }
		else{
			if (this.info.commandInfo){
            		treeheader.style.cursor='pointer';
            		treeheader.onclick = (ev:MouseEvent) => { application.OnCommand(this.info.commandInfo);
            	}
			}
		} 
        return treeheader;
    }
    
    private RenderChilds(){
        if(!this.nodes ||  this.nodes.length == 0){
            return undefined;
        }
        
        var treelist = Dom.div('treelist');
        this.nodes.forEach(node =>{ node.Render(treelist)});
        treelist.style.display = this.open? 'block' : 'none';        
        return treelist;
    }
}