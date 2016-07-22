import {IBoard, ISection} from './interfaces'
import {Section} from './Section';

class Board {
    private sections: {[key:string]:Section};
    private activeSectionId:string;
    private board:IBoard;
    
    constructor(board:IBoard){
        this.board = board;
    }
    
    public getID():string{
        return 'test';
    }
    
    public AddSection(section:Section){
        this.sections[section.id] = section;
    }    
}

export {Board}