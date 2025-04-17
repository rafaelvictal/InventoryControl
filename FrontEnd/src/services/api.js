import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});

export const registerMovement = async (data) => {
  try {
    const res = await api.post("/movements", data);
    return { success: true, data: res.data };
  } catch (err) {
    return { success: false, message: err.response?.data || err.message };
  }
};

export const getStockReport = async (date, code) => {
  try {
    const res = await api.get("/movements/report", {
      params: { date, code },
    });
    return { success: true, data: res.data };
  } catch (err) {
    return { success: false, message: err.response?.data || err.message };
  }
};
