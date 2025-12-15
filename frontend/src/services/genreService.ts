import { api } from "../api/api";
import type { Genre } from "../models/Genre";

export const genreService = {
  getAll: async (): Promise<Genre[]> => {
    const { data } = await api.get<Genre[]>("/Genres");
    return data;
  },

  create: async (name: string): Promise<Genre> => {
    const { data } = await api.post<Genre>("/Genres", { name });
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/Genres/${id}`);
  },
};
