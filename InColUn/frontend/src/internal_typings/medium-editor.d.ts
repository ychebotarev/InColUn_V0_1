declare class MediumEditor{
    constructor(elements:any, options?:any);
    public setup();
    public destroy();
    public on(target: any, event: any, listener: any, useCapture: boolean);
    public off(target: any, event: any, listener: any, useCapture:boolean)
    public subscribe(event: any, listener);
    public unsubscribe(event: any, listener);
    public trigger(name: any, data: any, editable:boolean);
    public delay(fn: any);
    public serialize();
    public getExtensionByName(name:string);
    public addBuiltInExtension(name:string, opts:any);
    public stopSelectionUpdates();
    public startSelectionUpdates ();
    public checkSelection();
    public queryCommandState (action:any);
    public execAction(action:any, opts:any);

    public getSelectedParentElement(range:any);

    public selectAllContents();
    public selectElement(element:any);
    public getFocusedElement ();
    public exportSelection();
    public saveSelection();
    public importSelection(selectionState:any, favorLaterSelectionAnchor:any);
    public restoreSelection();
    public createLink(opts);
    public cleanPaste(text);
    public pasteHTML(html:any, options:any);
    public setContent(html:any, index:any);        
    
    extensions:any;
}
    
declare module "medium-editor" {
    export = MediumEditor;
}

    