import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import AuthorsPage from "../pages/Authors/AuthorsPage";
import GenresPage from "../pages/Genres/GenresPage";
import BooksPage from "../pages/Books/BooksPage";
import { NavBar } from "../components/NavBar";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<Navigate to="/books" />} />
        <Route path="/authors" element={<AuthorsPage />} />
        <Route path="/genres" element={<GenresPage />} />
        <Route path="/books" element={<BooksPage />} />
      </Routes>
    </BrowserRouter>
  );
}
