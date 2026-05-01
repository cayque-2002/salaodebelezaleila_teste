import { useNavigate } from "react-router-dom";
import { LogOut } from "lucide-react";
import { getUser, clearUser } from "../../services/userStorage";
import "./Topbar.css";

export default function Topbar() {
  const navigate = useNavigate();
  const user = getUser();

  const logout = () => {
    clearUser();
    navigate("/");
  };

  return (
    <div className="topbar">
      <div className="left">
        {/* vazio de propósito (layout limpo) */}
      </div>

      <div className="right">
        <span className="user-name">{user?.nome}</span>

        <button className="logout-btn" onClick={logout}>
          <LogOut size={18} />
        </button>
      </div>
    </div>
  );
}