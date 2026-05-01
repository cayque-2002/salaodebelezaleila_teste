import { api } from "./api";

export const listarServicos = async () => {
  const res = await api.get("/api/Servico");
  return res.data;
};

export const criarServico = async (data: any) => {
  const res = await api.post("/api/Servico", data);
  return res.data;
};

export const atualizarServico = async (id: number, data: any) => {
  const res = await api.put(`/api/Servico/${id}`, data);
  return res.data;
};

export const deletarServico = async (id: number) => {
  const res = await api.delete(`/api/Servico/${id}`);
  return res.data;
};