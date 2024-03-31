import http from "../utils/http";


export const signin = (username, password) => {
    return http.post(`/login?username=${username}&password=${password}`);
};

export const Register = (data) => {
    return http.post("register", data);
};

export const GetInforUser = (username) => {
    return http.get(`userinfor?username=${username}`);
}