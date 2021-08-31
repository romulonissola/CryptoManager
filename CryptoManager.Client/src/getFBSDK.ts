import { environment } from './environments/environment';
declare const FB: any;
(<any>window).fbAsyncInit = ()=> {
    FB.init({
      appId            : environment.login.facebook.appId,
      xfbml            : false,
      version          : 'v2.12'
    });
    FB.AppEvents.logPageView();
};

(function(d, s, id){
    let js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));