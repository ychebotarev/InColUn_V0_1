export interface CommandInfo{
    command:string;
    param1?:{}
    param2?:{}
}

export interface OnCommandCallback{ (param1:{}, param2:{}):void }