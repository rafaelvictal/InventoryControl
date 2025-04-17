import React, { useState } from "react";
import {
  TextField,
  Button,
  Box,
  Typography,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Snackbar,
  Alert,
} from "@mui/material";
import { getStockReport } from "../services/api";

export default function StockReport() {
  const [form, setForm] = useState({ date: "", code: "" });
  const [data, setData] = useState([]);
  const [snackbar, setSnackbar] = useState({
    open: false,
    message: "",
    severity: "error",
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSearch = async () => {
    if (!form.date) {
      setSnackbar({
        open: true,
        message: "Please select a date.",
        severity: "error",
      });
      return;
    }

    const res = await getStockReport(form.date, form.code);
    if (res.success) {
      setData(res.data);
    } else {
      setSnackbar({
        open: true,
        message: res.message || "Error fetching report.",
        severity: "error",
      });
    }
  };

  return (
    <Box>
      <Typography variant="h6" gutterBottom>
        Stock Report
      </Typography>

      <Box display="flex" gap={2} alignItems="center" mb={2}>
        <TextField
          name="date"
          label="Date"
          type="date"
          InputLabelProps={{ shrink: true }}
          onChange={handleChange}
        />
        <TextField name="code" label="Product Code" onChange={handleChange} />
        <Button variant="contained" onClick={handleSearch}>
          Search
        </Button>
      </Box>

      {data.length > 0 && (
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Code</TableCell>
              <TableCell>Inbound</TableCell>
              <TableCell>Outbound</TableCell>
              <TableCell>Balance</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((item, index) => (
              <TableRow key={index}>
                <TableCell>{item.productName}</TableCell>
                <TableCell>{item.productCode}</TableCell>
                <TableCell>{item.inbound}</TableCell>
                <TableCell>{item.outbound}</TableCell>
                <TableCell>{item.balance}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      )}

      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert
          onClose={() => setSnackbar({ ...snackbar, open: false })}
          severity={snackbar.severity}
          sx={{ width: "100%" }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
