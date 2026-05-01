import { useEffect, useState } from "react";
import MainLayout from "../../layouts/mainLayout/MainLayout";
import { getDashboardSemanal } from "../../services/dashboardService";

import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
} from "chart.js";

import { Line, Bar } from "react-chartjs-2";

import "./Dashboard.css";

ChartJS.register(
  ArcElement,
  Tooltip,
  Legend,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement
);

export default function Dashboard() {
  const [data, setData] = useState<any>(null);

  const hoje = new Date();
  const [ano, setAno] = useState(hoje.getFullYear());
  const [semana, setSemana] = useState(getWeekNumber(hoje));

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const res = await getDashboardSemanal(semana, ano);
    setData(res);
  };

  function getWeekNumber(date: Date) {
    const firstJan = new Date(date.getFullYear(), 0, 1);
    const days = Math.floor(
      (date.getTime() - firstJan.getTime()) / (24 * 60 * 60 * 1000)
    );
    return Math.ceil((days + firstJan.getDay() + 1) / 7);
  }

  const lineData = {
    labels: data?.faturamentoPorDia?.map((x: any) => x.dia) || [],
    datasets: [
      {
        label: "Faturamento (R$)",
        data: data?.faturamentoPorDia?.map((x: any) => x.valor) || [],
        borderColor: "#3b82f6",
        backgroundColor: "#3b82f6",
        tension: 0.3,
      },
    ],
  };

  const barData = {
    labels: data?.servicosMaisFeitos?.map((x: any) => x.nome) || [],
    datasets: [
      {
        label: "Quantidade",
        data: data?.servicosMaisFeitos?.map((x: any) => x.quantidade) || [],
        backgroundColor: "#10b981",
      },
    ],
  };

  return (
    <MainLayout>
      <div className="container">

        {/* TITLE */}
        <h1>Dashboard Semanal</h1>

        {/* FILTERS */}
        <div className="filters">

          <select value={ano} onChange={(e) => setAno(Number(e.target.value))}>
            <option value={2025}>2025</option>
            <option value={2026}>2026</option>
          </select>

          <select value={semana} onChange={(e) => setSemana(Number(e.target.value))}>
            {Array.from({ length: 52 }).map((_, i) => (
              <option key={i + 1} value={i + 1}>
                Semana {i + 1}
              </option>
            ))}
          </select>

          <button onClick={load}>
            Aplicar
          </button>

        </div>

        {/* KPIs */}
        <div className="cards">

          <div className="card">
            <h3>Atendimentos</h3>
            <p>{data?.totalAtendimentos || 0}</p>
          </div>

          <div className="card">
            <h3>Faturamento</h3>
            <p>
              R$ {data?.faturamentoTotal?.toFixed(2) || "0,00"}
            </p>
          </div>

        </div>

        {/* GRÁFICO 1 */}
        <div className="chart-card">
          <h3>Faturamento por dia</h3>
          <Line data={lineData} />
        </div>

        {/* GRÁFICO 2 */}
        <div className="chart-card">
          <h3>Serviços mais feitos</h3>
          <Bar data={barData} />
        </div>

      </div>
    </MainLayout>
  );
}