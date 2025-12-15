import { useEffect, useState } from "react";
import type { Genre } from "../../models/Genre";
import { genreService } from "../../services/genreService";

export default function GenresPage() {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [name, setName] = useState("");
  const [loading, setLoading] = useState(false);

  async function loadGenres() {
    const data = await genreService.getAll();
    setGenres(data);
  }

  async function handleCreate() {
    if (!name.trim()) return;

    setLoading(true);
    await genreService.create(name);
    setName("");
    await loadGenres();
    setLoading(false);
  }

  async function handleDelete(id: number) {
    if (loading) return;
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this genre?"
    );
    if (!confirmDelete) return;

    setLoading(true);
    await genreService.delete(id);
    await loadGenres();
    setLoading(false);
  }

  useEffect(() => {
    const fetchGenres = async () => {
      setLoading(true);
      const data = await genreService.getAll();
      setGenres(data);
      setLoading(false);
    };

    fetchGenres();
  }, []);

  return (
    <div className="page-container">
      <h1 className="page-title">Genres</h1>

      <div className="page-form">
        <input
          placeholder="Genre name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          disabled={loading}
        />
        <button onClick={handleCreate} disabled={loading}>
          {loading ? "Loading..." : "Add"}
        </button>
      </div>

      {loading && <p>Loading genres...</p>}

      <section className="container">
        <div className="card-container">
          {genres.map((genre) => (
            <div className="card" key={genre.id}>
              <h3 className="card-title">{genre.name}</h3>
              <button onClick={() => handleDelete(genre.id)} disabled={loading}>
                Delete
              </button>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}
