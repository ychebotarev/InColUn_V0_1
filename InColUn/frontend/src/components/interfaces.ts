export interface ISection{
    id:string,
    title:string,
    childs:ISection[]
}

export interface IBoard{
    id:string,
    title:string,
    created:Date,
    updated:Date,
	sections?:ISection[]
}