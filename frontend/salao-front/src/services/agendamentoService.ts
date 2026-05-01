import { api } from "./api";

export const listarAgendamentos = async () => {
  const res = await api.get("/api/Agendamento");
  return res.data;
};

export const criarAgendamento = async (data: any) => {
  const res = await api.post("/api/Agendamento", data);
  return res.data;
};

export const atualizarAgendamento = async (id: number, data: any) => {
  const res = await api.put(`/api/Agendamento/${id}`, data);
  return res.data;
};

export const deletarAgendamento = async (id: number) => {
  const res = await api.delete(`/api/Agendamento/${id}`);
  return res.data;
};

export const listarMeusAgendamentos = async (usuarioId: number) => {
  const response = await api.get("/api/Agendamento/meus", {
    params: { usuarioId },
  });

  return response.data;
};