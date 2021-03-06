import { UserManager, WebStorageStateStore } from 'oidc-client';

export default class SecurityService {

    userManager;
 
    constructor() {

        const config = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            authority: "https://localhost:5001",
            client_id: 'vue',
            client_secret: 'vue_secret',
            offline_access: true,
            redirect_uri: 'http://localhost:8080/callback.html',
            popup_redirect_uri: 'http://localhost:8080/callback.html',
            automaticSilentRenew: true,
            silent_redirect_uri: 'http://localhost:8080/silent-renew.html',
            response_type: 'code',
            scope: 'openid profile api1 offline_access',
            post_logout_redirect_uri: 'http://localhost:8080/',
            filterProtocolClaims: true,
        };
 
        this.userManager = new UserManager(config);
    }
 
    getUser() {
        return this.userManager.getUser();
    }
 
    login() {
        // return this.userManager.signinRedirect();
        return this.userManager.signinPopup();
    }
 
    logout() {
        return this.userManager.signoutRedirect();
    }
 
    getAccessToken() {
        return this.userManager.getUser().then(data => {
            return data.access_token;
        });
    }
}