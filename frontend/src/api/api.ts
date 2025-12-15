import axios from "axios";

const isDocker = window.location.hostname !== "localhost";

const baseURL = isDocker
  ? import.meta.env.VITE_API_BASE_URL_DOCKER
  : import.meta.env.VITE_API_BASE_URL_LOCAL;

export const api = axios.create({
  baseURL,
});
