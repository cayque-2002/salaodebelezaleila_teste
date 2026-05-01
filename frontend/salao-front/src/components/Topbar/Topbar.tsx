import { getUser } from "../../services/userStorage";
import "./Topbar.css";

export default function Topbar() {
  const user = getUser();

  return (
    <div className="topbar">
      <div>Dashboard</div>

      <div>
        {user?.nome}
      </div>
    </div>
  );
}