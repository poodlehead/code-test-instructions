# Base image
FROM node:20-alpine

# Set working directory
WORKDIR /app

# Copy package.json and lock file
COPY url-shortner/package*.json ./

# Install dependencies
RUN npm install

# # Copy source code
# COPY url-shortner/ ./

# RUN npm run build

# FROM node:20-alpine

# WORKDIR /app

# Expose Next.js port
EXPOSE 3000

# Start Next.js dev server
CMD ["npm", "run", "dev"]
