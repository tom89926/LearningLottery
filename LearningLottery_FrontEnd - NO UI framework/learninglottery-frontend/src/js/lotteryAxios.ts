import axios, { type AxiosInstance } from 'axios';

const axiosDefaults = {
  baseURL: '',
  headers: { 'Content-Type': 'application/json' },
  timeout: 20000,
};
const lotteryAxios: AxiosInstance = axios.create(axiosDefaults);

lotteryAxios.interceptors.request.use(
  function (config) {
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);

lotteryAxios.interceptors.response.use(
  function (response) {
    // Do something with response data
    return response;
  },
  function (error) {
    if (error.response) {
      null;
    }
    if (!window.navigator.onLine) {
      alert('internet error');
      return;
    }
    return Promise.reject(error);
  }
);

export default lotteryAxios;
