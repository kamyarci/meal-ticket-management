import axios, { AxiosError } from 'axios';

const api = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:8080',
    headers: { 'Content-Type': 'application/json' },
});
api.interceptors.response.use(
    response => response,
    (error: AxiosError<{ message?: string }>) => {
        const message = error.response?.data?.message ?? 'Erro inesperado.';
        return Promise.reject(new Error(message));
    }
);
export default api;
