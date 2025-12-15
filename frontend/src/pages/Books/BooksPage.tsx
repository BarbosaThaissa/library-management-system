import { useEffect, useState } from "react";
import type { Book } from "../../models/Book";
import type { Author } from "../../models/Author";
import type { Genre } from "../../models/Genre";
import { bookService } from "../../services/bookService";
import { authorService } from "../../services/authorService";
import { genreService } from "../../services/genreService";

export default function BooksPage() {
  const [books, setBooks] = useState<Book[]>([]);
  const [authors, setAuthors] = useState<Author[]>([]);
  const [genres, setGenres] = useState<Genre[]>([]);

  const [title, setTitle] = useState("");
  const [authorId, setAuthorId] = useState<number | "">("");
  const [genreId, setGenreId] = useState<number | "">("");

  const [loading, setLoading] = useState(false);

  async function loadData() {
    const [booksData, authorsData, genresData] = await Promise.all([
      bookService.getAll(),
      authorService.getAll(),
      genreService.getAll(),
    ]);

    setBooks(booksData);
    setAuthors(authorsData);
    setGenres(genresData);
  }

  async function handleCreate() {
    if (!title.trim() || !authorId || !genreId) return;

    setLoading(true);

    await bookService.create({
      title,
      authorId,
      genreId,
    });

    setTitle("");
    setAuthorId("");
    setGenreId("");

    await loadData();
    setLoading(false);
  }

  async function handleDelete(id: number) {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this book?"
    );
    if (!confirmDelete) return;

    setLoading(true);
    await bookService.delete(id);
    await loadData();
    setLoading(false);
  }

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      await loadData();
      setLoading(false);
    };

    fetchData();
  }, []);

  return (
    <div className="page-container">
      <h1 className="page-title">Books</h1>

      <div className="page-form">
        <input
          placeholder="Book title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          disabled={loading}
        />

        <select
          value={authorId}
          onChange={(e) => setAuthorId(Number(e.target.value))}
          disabled={loading}
        >
          <option value="">Select author</option>
          {authors.map((a) => (
            <option key={a.id} value={a.id}>
              {a.name}
            </option>
          ))}
        </select>

        <select
          value={genreId}
          onChange={(e) => setGenreId(Number(e.target.value))}
          disabled={loading}
        >
          <option value="">Select genre</option>
          {genres.map((g) => (
            <option key={g.id} value={g.id}>
              {g.name}
            </option>
          ))}
        </select>

        <button onClick={handleCreate} disabled={loading}>
          {loading ? "Saving..." : "Add"}
        </button>
      </div>

      {loading && <p>Loading books...</p>}

      <section className="container">
        <div className="card-container">
          {books.map((book) => (
            <div className="card" key={book.id}>
              <h3 className="card-title">{book.title}</h3>
              <p className="card-author">Author: {book.authorName}</p>
              <p className="card-genre">Genre: {book.genreName}</p>
              <button onClick={() => handleDelete(book.id)} disabled={loading}>
                Delete
              </button>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}
