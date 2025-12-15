import type { Book } from "../models/Book";
import { api } from "../api/api";

interface CreateBookDto {
  title: string;
  authorId: number;
  genreId: number;
}

export const bookService = {
  getAll: async (): Promise<Book[]> => {
    const { data } = await api.get<Book[]>("/Books");
    return data;
  },

  create: async (dto: CreateBookDto): Promise<Book> => {
    const { data } = await api.post<Book>("/Books", dto);
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/Books/${id}`);
  },
};
