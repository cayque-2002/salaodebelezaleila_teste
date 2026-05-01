import { api } from "./api";

export const getDashboardSemanal = async (semana: number, ano: number) => {
  const res = await api.get(
    `/api/Agendamento/dashboard/semanal?semana=${semana}&ano=${ano}`
  );

  return res.data;
};