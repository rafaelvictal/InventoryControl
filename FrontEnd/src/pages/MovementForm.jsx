import React, { useState } from "react";
import { useForm } from "react-hook-form";
import {
  TextField,
  Button,
  MenuItem,
  Box,
  Typography,
  Snackbar,
  Alert,
} from "@mui/material";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { registerMovement } from "../services/api";

const schema = yup.object().shape({
  productCode: yup.string().required("Product code is required"),
  type: yup.string().oneOf(["Inbound", "Outbound"]).required(),
  quantity: yup
    .number()
    .typeError("Quantity must be a number greater than 0")
    .positive("Quantity must be greater than 0")
    .integer("Quantity must be an integer")
    .required("Quantity is required")
    .max(9999, "Maximum allowed is 9999"),
});

export default function MovementForm() {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm({ resolver: yupResolver(schema) });

  const [snackbar, setSnackbar] = useState({
    open: false,
    message: "",
    severity: "success", // "success" | "error"
  });

  const onSubmit = async (data) => {
    const res = await registerMovement(data);
    if (res.success) {
      setSnackbar({
        open: true,
        message: "Movement registered successfully.",
        severity: "success",
      });
      reset();
    } else {
      setSnackbar({
        open: true,
        message: res.message || "Something went wrong.",
        severity: "error",
      });
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      noValidate
      sx={{ mb: 4 }}
    >
      <Typography variant="h6" gutterBottom>
        Register Movement
      </Typography>

      <TextField
        label="Product Code"
        fullWidth
        margin="normal"
        {...register("productCode")}
        error={!!errors.productCode}
        helperText={errors.productCode?.message}
      />

      <TextField
        label="Type"
        select
        fullWidth
        margin="normal"
        {...register("type")}
        error={!!errors.type}
        helperText={errors.type?.message}
      >
        <MenuItem value="Inbound">Inbound</MenuItem>
        <MenuItem value="Outbound">Outbound</MenuItem>
      </TextField>

      <TextField
        label="Quantity"
        type="number"
        fullWidth
        margin="normal"
        {...register("quantity")}
        error={!!errors.quantity}
        helperText={errors.quantity?.message}
      />

      <Button type="submit" variant="contained" disabled={isSubmitting}>
        Submit
      </Button>

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
