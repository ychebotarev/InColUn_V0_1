/// <reference path='../typings/index.d.ts' />

function successAuth(result, errorBlock, errorMsg){
  if(result.success){
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
	$.ajax('/auth/facebook',  {
		type     : 'GET', 
		data     : {}, 
		dataType : 'json',
		success  : successLogin
	});  
}

$(document).ready(function ($) {
  $('#form-login').submit(function(event) {
        var data = {
            email    : $('#i-login-email').val(),
            password : $('#i-login-pwd').val()
        };

        $.ajax('auth/login', {
            type     : 'POST', 
            data     : data, 
            dataType : 'json',
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

        $.ajax('auth/signup', {
            type     : 'POST', 
            data     : data, 
            dataType : 'json',
            success  : successSignup
        });  
        event.preventDefault();
  });
});
