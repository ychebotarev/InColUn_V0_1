export interface IBoardInfo{
	title:string,
	created:Date,
	updated:Date,
	shared:string,	
	saved:number,
	kudos:number
}

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
    shared?:number,
    saved?:number,
    kudos?:number,
	sections?:ISection[]
}