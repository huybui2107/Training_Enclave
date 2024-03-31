import axios from "axios";
import { config } from "npm";
class Http {
    constructor() {
        this.instance = axios.create({
            baseURL: "http://localhost:5242/api/",
            name: "Dictionary App",
            timeout: 10000,
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
        });
        this.instance.interceptors.response.use(
            (response) => {
                return response.data;
            },
            ({ response }) => {
                // if (response.status === 400) {
                //     return Promise.reject(response.data);
                // }
                const result = { ...response.data };
                return Promise.reject(result);
            }
        );
        this.instance.interceptors.request.use(

            (config) => {
                console.log(config)
                const token = getTokenFromStorage();
                if (token) {
                    config.headers["Authorization"] = `Bearer ${token}`;
                }
                return config;
            },
            (error) => {
                return Promise.reject(error.response);
            }
        );
    }
    get(url, config = null) {
        return this.instance.get(url, config);
    }
    post(url, data, config = null) {
        return this.instance.post(url, data, config);
    }
    put(url, data, config = null) {
        return this.instance.put(url, data, config);
    }
    patch(url, data, config = null) {
        return this.instance.patch(url, data, config);
    }
    delete(url, data, config = null) {
        return this.instance.delete(url, {
            data,
            ...config,
        });
    }
}

const http = new Http();

export default http;