import {CommandInfo} from '../../core/CommandInfo'
 
export interface TreeNodeInfo{
    title:string;
    guid:string;
    commandInfo?:CommandInfo;
} 
 
export interface TreeNodeData{
    info:TreeNodeInfo
    subNodes?:TreeNodeData[];
}