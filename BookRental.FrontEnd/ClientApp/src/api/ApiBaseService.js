import Axios from 'axios';

export default class BaseApiService {
    baseApiUrl = "https://localhost:44375/api/";

    constructor(controller) {
        this.baseApiUrl += `${controller}`;
    }


    getRequest(url) {
        return new Promise((resolve, reject) => {
            Axios.get(url)
                .then(response => {
                    if (response.data.value.isSuccess) {
                        resolve(response.data.value);
                    } else {
                        reject(response.data.value);
                    }
                })
                .catch(error => {
                    reject(error);
                });
        });
    }

    postRequest(url, jsonData) {
        return new Promise((resolve, reject) => {
            Axios.post(url, jsonData, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    if (response.data.value.isSuccess) {
                        resolve(response.data.value);
                    } else {
                        reject(response.data.value);
                    }
                })
                .catch(error => {
                    reject(error);
                });
        });
    }

    putRequest(url, jsonData) {
        return new Promise((resolve, reject) => {
            Axios.put(url, jsonData, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    if (response.data.value.isSuccess) {
                        resolve(response.data.value);
                    } else {
                        reject(response.data.value);
                    }
                })
                .catch(error => {
                    reject(error);
                });
        });
    }

    deleteRequest(url, jsonData) {
        return new Promise((resolve, reject) => {
            Axios.delete(url)
                .then(response => {
                    if (response.data.value.isSuccess) {
                        resolve(response.data.value);
                    } else {
                        reject(response.data.value);
                    }
                })
                .catch(error => {
                    reject(error);
                });
        });
    }

    getList() {
        return this.getRequest(this.baseApiUrl);
    }

    get(id) {
        const url = `${this.baseApiUrl}/${id}`;
        return this.getRequest(url);
    }

    post(data) {
        const jsonData = JSON.stringify(data);
        return this.postRequest(this.baseApiUrl, jsonData);
    }

    put(id, data) {
        const url = `${this.baseApiUrl}/${id}`;
        const jsonData = JSON.stringify(data);
        return this.putRequest(url, jsonData);
    }

    delete(id) {
        const url = `${this.baseApiUrl}/${id}`;
        return this.deleteRequest(url);
    }
}
