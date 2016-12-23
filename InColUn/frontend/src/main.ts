import {application} from './App';
import {Board} from './components/Board';

application.SetRoot(document.getElementById('app'));
application.Render();
application.Initialize();

$(document).ready(function ($) {
	$('#newBoardModalCreateBoard').click(function(){
        application.OnCommand({command:'CreateNewBoardDialogOk'});
    });
})
