import MainLayout from "../../layouts/mainLayout/MainLayout";

export default function Dashboard() {
  return (
    <MainLayout>
      <h1>Dashboard</h1>

      <div style={{ display: "flex", gap: 10 }}>
        <div style={cardStyle}>Clientes</div>
        <div style={cardStyle}>Agendamentos</div>
        <div style={cardStyle}>Serviços</div>
      </div>
    </MainLayout>
  );
}

const cardStyle = {
  background: "white",
  padding: 20,
  borderRadius: 10,
  flex: 1,
};