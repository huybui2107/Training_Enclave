import axios from "axios";

class Http {
  constructor() {
    this.instance = axios.create({
      baseURL: "http://192.168.20.116:5242/api/",
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
        if (response.status === 400) {
          return Promise.reject(response.data);
        }
        const result = { ...response.data };
        return Promise.reject(result);
      }
    );
    this.instance.interceptors.request.use(
      (config) => {
        // const token = getTokenFromStorage();
        var accessToken = localStorage.getItem("user");
        if (accessToken) {
          config.headers["Authorization"] = `Bearer ${JSON.parse(accessToken)}`;
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
