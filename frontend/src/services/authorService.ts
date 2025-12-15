import { api } from "../api/api";
import type { Author } from "../models/Author";

export const authorService = {
  getAll: async (): Promise<Author[]> => {
    const { data } = await api.get<Author[]>("/Authors");
    return data;
  },

  create: async (name: string): Promise<Author> => {
    const { data } = await api.post<Author>("/Authors", { name });
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/Authors/${id}`);
  },
};
