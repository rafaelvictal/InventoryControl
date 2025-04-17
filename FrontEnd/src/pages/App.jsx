import React from "react";
import MovementForm from "./MovementForm";
import StockReport from "./StockReport";
import { Container, Typography, Divider } from "@mui/material";
import Inventory2Icon from "@mui/icons-material/Inventory2";

export default function App() {
  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography
        variant="h4"
        gutterBottom
        display="flex"
        alignItems="center"
        gap={1}
      >
        <Inventory2Icon fontSize="large" />
        Inventory Control
      </Typography>
      <MovementForm />
      <Divider sx={{ my: 4 }} />
      <StockReport />
    </Container>
  );
}
