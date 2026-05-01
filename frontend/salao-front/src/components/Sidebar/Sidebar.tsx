import { useLocation, useNavigate } from "react-router-dom";
import "./Sidebar.css";

const getUser = () => {
  const user = localStorage.getItem("user");
  return user ? JSON.parse(user) : null;
};

const isAdmin = () => getUser()?.tipo === 1;

export default function Sidebar() {
  const navigate = useNavigate();
  const location = useLocation();

  const isActive = (path: string) => location.pathname === path;

  return (
    <div className="sidebar">

      <h2 className="logo">Salão</h2>

      <ul className="menu">

        

        {/* 👇 ADMIN ONLY */}
        {isAdmin() && (
          <li
            className={isActive("/dashboard") ? "active" : ""}
            onClick={() => navigate("/dashboard")}
          >
            Dashboard
          </li>
        )}

        {isAdmin() && (
          <li
            className={isActive("/clientes") ? "active" : ""}
            onClick={() => navigate("/clientes")}
          >
            Clientes
          </li>
        )}

        {/* 👇 ADMIN ONLY */}
        {isAdmin() && (
          <li
            className={isActive("/servicos") ? "active" : ""}
            onClick={() => navigate("/servicos")}
          >
            Serviços
          </li>
        )}

        <li
          className={isActive("/agendamentos") ? "active" : ""}
          onClick={() => navigate("/agendamentos")}
        >
          Agendamentos
        </li>

      </ul>

    </div>
  );
}