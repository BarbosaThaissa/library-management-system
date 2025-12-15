import { Link } from "react-router-dom";
import "./NavBar.css"; // opcional, se quiser estilizar

export function NavBar() {
  return (
    <nav className="navbar">
      <ul>
        <li>
          <Link to="/authors">Authors</Link>
        </li>
        <li>
          <Link to="/genres">Genres</Link>
        </li>
        <li>
          <Link to="/books">Books</Link>
        </li>
      </ul>
    </nav>
  );
}
