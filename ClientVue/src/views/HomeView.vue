<template>
  <div class="home">
    <button id="login" @click="login">Login</button>
    <button id="api" @click="api">Call API</button>
    <button id="logout" @click="logout">Logout</button>
  </div>
</template>

<script>
import SecurityService from "@/services/SecurityService";
export default {
  setup() {
    const mgr = new SecurityService();
    const login = () => {
      mgr.login();
    };

    const logout = () => {
      mgr.logout();
    };

    const api = () => {
      mgr.getAccessToken().then((userToken) => {
        let url = "https://localhost:6001/api/identity";
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
          console.log(xhr.status, JSON.parse(xhr.responseText));
        };
        xhr.setRequestHeader("Authorization", "Bearer " + userToken);
        xhr.send();
      });
    };

    return {
      api,
      login,
      logout,
    };
  },
};
</script>
