function successAuth(result, errorBlock, errorMsg){
  if(result.success){
    console.log(result.token);
    document.cookie = "access_token="+result.token;
    window.location.replace('/boards');
  }else{
    $(errorMsg).text(result.message);  
    $(errorBlock).show();  
  }
}

function successSignup(result){
  successAuth(result, '#signupError', '#signupErrorMessage')
}

function successLogin(result){
  successAuth(result, '#loginError', '#loginErrorMessage')
}

function facebookLoginAttempt(){
	$.ajax({
		type     : 'GET', 
		url      : '/auth/facebook', 
		data     : {}, 
		dataType : 'json',
		encode   : true,
		success  : successLogin
	});  
}

$(document).ready(function ($) {
  $('#form-login').submit(function(event) {
        var data = {
            email    : $('#i-login-email').val(),
            password : $('#i-login-pwd').val()
        };

        $.ajax({
            type     : 'POST', 
            url      : 'auth/login', 
            data     : data, 
            dataType : 'json',
            encode   : true,
            success  : successLogin
        });  
        event.preventDefault();
  });
  
  $('#form-signup').submit(function(event) {
        var data = {
            name     : $('#i-signup-name').val(),
            email    : $('#i-signup-email').val(),
            password : $('#i-signup-pwd').val()
        };

        $.ajax({
            type     : 'POST', 
            url      : 'auth/signup', 
            data     : data, 
            dataType : 'json',
            encode   : true,
            success  : successSignup
        });  
        event.preventDefault();
  });
});
