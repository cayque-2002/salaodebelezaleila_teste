import Sidebar from "../../components/Sidebar/Sidebar";
import Topbar from "../../components/Topbar/Topbar";
import "./MainLayout.css";

export default function MainLayout({ children }: any) {
  return (
    <div className="layout">
      <Sidebar />

      <div className="main">
        <Topbar />

        <div className="content">
          {children}
        </div>
      </div>
    </div>
  );
}