import { useEffect, useState } from "react";
import type { Author } from "../../models/Author";
import { authorService } from "../../services/authorService";

export default function AuthorsPage() {
  const [authors, setAuthors] = useState<Author[]>([]);
  const [name, setName] = useState("");
  const [loading, setLoading] = useState(false);

  async function loadAuthors() {
    const data = await authorService.getAll();
    setAuthors(data);
  }

  async function handleCreate() {
    if (!name.trim()) return;

    setLoading(true);
    await authorService.create(name);
    setName("");
    await loadAuthors();
    setLoading(false);
  }

  async function handleDelete(id: number) {
    if (loading) return;
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this author?"
    );
    if (!confirmDelete) return;

    setLoading(true);
    await authorService.delete(id);
    await loadAuthors();
    setLoading(false);
  }

  useEffect(() => {
    const fetchAuthors = async () => {
      setLoading(true);
      const data = await authorService.getAll();
      setAuthors(data);
      setLoading(false);
    };

    fetchAuthors();
  }, []);

  return (
    <div className="page-container">
      <h1 className="page-title">Authors</h1>

      <div className="page-form">
        <input
          placeholder="Author name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          disabled={loading}
        />
        <button onClick={handleCreate} disabled={loading}>
          {loading ? "Loading..." : "Add"}
        </button>
      </div>

      {loading && <p>Loading authors...</p>}

      <section className="container">
        <div className="card-container">
          {authors.map((author) => (
            <div className="card" key={author.id}>
              <h3 className="card-title">{author.name}</h3>
              <button
                onClick={() => handleDelete(author.id)}
                disabled={loading}
              >
                Delete
              </button>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}
