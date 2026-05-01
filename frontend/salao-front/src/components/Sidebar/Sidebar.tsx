import "./Sidebar.css";
import { useNavigate } from "react-router-dom";

export default function Sidebar() {
  const navigate = useNavigate();

  return (
    <div className="sidebar">
      <h2>Salão</h2>

      <ul>
        <li onClick={() => navigate("/dashboard")}>Dashboard</li>
        <li onClick={() => navigate("/clientes")}>Clientes</li>
        <li onClick={() => navigate("/servicos")}>Serviços</li>
        <li onClick={() => navigate("/agendamentos")}>Agendamentos</li>
      </ul>
    </div>
  );
}   